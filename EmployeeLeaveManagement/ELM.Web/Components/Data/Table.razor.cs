using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace ELM.Web.Components.Data
{
	public partial class Table<TItem>
	{
		/// <summary>
		/// Local variables
		/// </summary>
		private HashSet<BaseColumn> columns;
		private int currentPageNumber = 1;

		private string filterCriteria = "";
		private int selectedIndex = -1;



		/// <summary>
		/// Injeting js runtime
		/// </summary>
		[Inject]
		public IJSRuntime JS { get; set; }



		/// <summary>
		/// Determines if delete button is displayed or not
		/// </summary>
		[Parameter]
		public bool AllowDelete { get; set; } = false;



		/// <summary>
		/// Determines if edit button is displayed or not
		/// </summary>
		[Parameter]
		public bool AllowEdit { get; set; } = false;



		/// <summary>
		/// Determines if user can select row in UI
		/// </summary>
		[Parameter]
		public bool AllowSelect { get; set; } = false;



		/// <summary>
		/// Placeholder for all table columns
		/// </summary>
		[Parameter]
		public RenderFragment Columns { get; set; }



		/// <summary>
		/// the list of items to display in table
		/// </summary>
		[Parameter]
		public List<TItem> Items { get; set; }



		/// <summary>
		/// Event callback when user deletes item
		/// </summary>
		[Parameter]
		public EventCallback<TItem> OnDelete { get; set; }



		/// <summary>
		/// Event callback when user starts editing item
		/// </summary>
		[Parameter]
		public EventCallback<TItem> OnEdit { get; set; }



		/// <summary>
		/// Determines if table border with padding is to be shown or not
		/// </summary>
		[Parameter]
		public bool ShowBorder { get; set; } = true;



		/// <summary>
		/// CSS style for the component
		/// </summary>
		[Parameter]
		public string Style { get; set; }



		/// <summary>
		/// Evetn fired after rendering component
		/// </summary>
		/// <param name="firstRender"></param>
		protected override void OnParametersSet()
		{
			if (AllowSelect && Items is not null && Items.Count > 0 && selectedIndex == -1)
			{
				SelectItem(Items[0], true, 0);
			}
		}



		/// <summary>
		/// Called from TableColumn component to register itself in table
		/// </summary>
		/// <param name="column"></param>
		public void RegisterColumn(BaseColumn column)
		{
			if (columns == null) columns = new();
			columns.Add(column);
			StateHasChanged();
		}



		/// <summary>
		/// Called from TableColumn component to unregister itself form table
		/// </summary>
		/// <param name="column"></param>
		public void UnregisterColumn(BaseColumn column)
		{
			columns.Remove(column);
			StateHasChanged();
		}



		/// <summary>
		/// Called when user clicks on edit
		/// </summary>
		/// <param name="item">the item to be edited</param>
		private void Edit(TItem item)
		{
			OnEdit.InvokeAsync(item);
		}



		/// <summary>
		/// Called when user clicks on edit
		/// </summary>
		/// <param name="item">The item to be deleted</param>
		private async Task Delete(TItem item)
		{
			bool confirm = await JS.InvokeAsync<bool>("confirm", "Do you really want to delete?");

			if (confirm)
			{
				await OnDelete.InvokeAsync(item);
			}
		}



		/// <summary>
		/// Returns concatenated string of all colummns in given row
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private string RowString(TItem item)
		{
			string val = "";

			foreach (var column in columns.Where(x => !String.IsNullOrEmpty(x.DataField) && x.Searchable && x.Visible))
			{
				val = val + " " + Helper.GetPropertyValue<string>(item, column.DataField);
			}

			return val;
		}



		/// <summary>
		/// Selects item in UI
		/// </summary>
		/// <param name="item"></param>
		/// <param name="ans"></param>
		private void SelectItem(TItem item, bool ans, int idx)
		{
			Items = Items.Select(x =>
			{
				Helper.SetPropertyValue<bool>(x, "Selected", false);
				return x;
			}).ToList();

			// Select given item
			Helper.SetPropertyValue<bool>(item, "Selected", ans);
			selectedIndex = idx;
		}



		/// <summary>
		/// Applies filter in table
		/// </summary>
		/// <param name="criteria">the search criteria for filter</param>
		public int ApplyFilter(string criteria)
		{
			filterCriteria = criteria;
			return Items?.Count(x => RowString(x).ToLower().Contains(filterCriteria.ToLower())) ?? 0;
		}



		/// <summary>
		/// Return currently selected item
		/// </summary>
		/// <returns></returns>
		public TItem SelectedItem() => Items[selectedIndex];
	}
}

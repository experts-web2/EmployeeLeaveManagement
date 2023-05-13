using Microsoft.AspNetCore.Components;

namespace ELM.Web.Components.Data
{
	public partial class TextColumn<TItem> : BaseColumn, IDisposable
	{
		/// <summary>
		/// The Table component reference 
		/// </summary>
		[CascadingParameter]
		protected Table<TItem> Context { get; set; }



		/// <summary>
		/// Event fired on initializing component
		/// </summary>
		protected override void OnInitialized() => Context.RegisterColumn(this);



		/// <summary>
		/// Called on disposing component
		/// </summary>
		public void Dispose() => Context.UnregisterColumn(this);
	}
}

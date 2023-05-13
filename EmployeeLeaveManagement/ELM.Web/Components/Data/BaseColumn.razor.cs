using Microsoft.AspNetCore.Components;


namespace ELM.Web.Components.Data
{
    public partial class BaseColumn
	{
		/// <summary>
		/// Determines column heading text
		/// </summary>
		[Parameter]
		public string Heading { get; set; }



		/// <summary>
		/// Determines the field form object to use to dsplay data
		/// </summary>
		[Parameter]
		public string DataField { get; set; }



		/// <summary>
		/// Determines if this column is searchable
		/// </summary>
		[Parameter]
		public bool Searchable { get; set; } = true;



		/// <summary>
		/// Dtermines text alignment in column
		/// </summary>
		[Parameter]
		public TextAlign TextAlign { get; set; } = TextAlign.Left;



		/// <summary>
		/// Determines if this column is visible
		/// </summary>
		[Parameter]
		public bool Visible { get; set; } = true;
	}
}

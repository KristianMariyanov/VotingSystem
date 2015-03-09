namespace VotingSystem.Web.Infrastructure.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;

    public static class KendoGridHelper
    {
        public static GridBuilder<T> FullFeaturedGrid<T>(this HtmlHelper helper, string controllerName, Expression<Func<T, object>> modelIdExpression, Action<GridColumnFactory<T>> columns = null, Action<DataSourceModelDescriptorFactory<T>> model = null) where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                    cols.Command(c => c.Edit());
                    cols.Command(c => c.Destroy());
                };
            }

            return helper.Kendo()
                         .Grid<T>()
                         .Name("grid")
                         .Columns(columns)
                         .ToolBar(toolbar => toolbar.Create())
                         .ColumnMenu()
                         .Pageable(page => page.Refresh(true))
                         .Sortable()
                         .Filterable()
                         .Editable(edit => edit.Mode(GridEditMode.PopUp))
                         .Resizable(resize => resize.Columns(true))
                         .DataSource(data => data
                                                 .Ajax()
                                                 .Model(m => m.Id(modelIdExpression))
                                                 .ServerOperation(true)
                                                 .Events(ev => ev.Error("errorHandler"))
                                                 .Read(read => read.Action("Read", controllerName))
                                                 .Create(create => create.Action("Create", controllerName))
                                                 .Update(update => update.Action("Update", controllerName))
                                                 .Destroy(destroy => destroy.Action("Destroy", controllerName)));
        }

        public static GridBuilder<T> ReadAndDeleteGrid<T>(this HtmlHelper helper, string controllerName, Expression<Func<T, object>> modelIdExpression, Action<GridColumnFactory<T>> columns = null) where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                    cols.Command(c => c.Edit());
                    cols.Command(c => c.Destroy());
                };
            }

            return helper.Kendo()
                         .Grid<T>()
                         .Name("grid")
                         .Columns(columns)
                         .ColumnMenu()
                         .Pageable(page => page.Refresh(true))
                         .Sortable()
                         .Groupable()
                         .Filterable()
                         .Editable(edit => edit.Mode(GridEditMode.PopUp))
                         .Resizable(resize => resize.Columns(true))
                         .DataSource(data => data
                                                 .Ajax()
                                                 .Model(m => m.Id(modelIdExpression))
                                                 .ServerOperation(true)
                                                 .Events(ev => ev.Error("errorHandler"))
                                                 .Read(read => read.Action("Read", controllerName))
                                                 .Update(update => update.Action("Update", controllerName))
                                                 .Destroy(destroy => destroy.Action("Destroy", controllerName)));
        }
    }
}
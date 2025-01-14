using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Soporte
{
    public class ComponenteAngularHTML
    {
        private Tabla tabla;
        public ComponenteAngularHTML(Tabla tabla)
        {
            this.tabla = tabla;
        }

        public string listadoPrincipal()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
        <table mat-table *ngIf=""this._seleccion == null"" [dataSource]=""dataSource"" class=""mat-elevation-z8"">
");

            foreach(Columna c in tabla.Columnas) sb.Append(@"
            <ng-container matColumnDef=" + c.COLUMN_NAME + @">
                <th mat-header-cell *matHeaderCellDef>" + c.COLUMN_NAME + @"</th>
                <td mat-cell *matCellDef=""let " + tabla.name.ToLower() + @""">{{" + tabla.name.ToLower() + @"." + c.COLUMN_NAME + @"}}</td>
            </ng-container>
");

            sb.Append(@"
            <tr mat-header-row *matHeaderRowDef=""displayedColumns""></tr>
            <tr mat-row *matRowDef=""let row; columns: displayedColumns;"" (click)=""this._seleccionar(row)""></tr>

        </table>
        <mat-paginator *ngIf=""this._seleccion == null"" [length]=""(dataSource == null || dataSource == undefined) ? 0 : dataSource.length""
                      [pageSize]=""10""
                      [pageSizeOptions]=""[5, 10, 25, 100]""
                      aria-label=""Select page"">
        </mat-paginator>
");
            return sb.ToString();
        }

        public string agregarPrincipal()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"

        <form *ngIf=""this._seleccion != null"" class=""example-form"">
");

            foreach(Columna columna in tabla.Columnas) sb.Append(@"
            <mat-form-field class=""example-full-width"">
                <mat-label>" + columna.COLUMN_NAME + @"</mat-label>
                <input matInput>
            </mat-form-field>
");

            sb.Append(@"
        </form>

");
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            res.Append(@"
<mat-card appearance=""outlined"">
    <mat-card-header>
        <mat-card-title>" + tabla.name + @"</mat-card-title>
        <mat-card-subtitle>Descripci&oacute;n del cat&aacute;logo</mat-card-subtitle>
    </mat-card-header>
    <mat-card-content>

");
            res.Append(listadoPrincipal());
            res.Append(agregarPrincipal());
            res.Append(@"

    </mat-card-content>
    <mat-card-actions>
        <button mat-button *ngIf=""this._seleccion == null"" (click)=""this._agregar()"">Agregar</button>

        <button mat-flat-button *ngIf=""this._seleccion != null"" (click)=""this._aceptar()"">Aceptar</button>
        <button mat-button *ngIf=""this._seleccion != null"" (click)=""this._cancelar()"">Cancelar</button>
    </mat-card-actions>
    <mat-card-footer></mat-card-footer>
</mat-card>
");
            return res.ToString();
        }
    }
}

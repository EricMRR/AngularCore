using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Soporte
{
    public class ComponenteAngularTS
    {
        private Tabla tabla;
        public ComponenteAngularTS(Tabla tabla)
        {
            this.tabla = tabla;
        }

        private string name
        {
            get
            {
                return tabla.name.Substring(0, 1).ToUpper() + ((tabla.name != null && tabla.name.Length > 1) ? tabla.name.Substring(1, tabla.name.Length - 1) : "");
            }
        }

        private string _columnas() {
            StringBuilder res = new StringBuilder();

            foreach (Columna c in tabla.Columnas)
            {
                if(res.Length > 0) res.Append(", ");
                res.Append("'" + c.COLUMN_NAME + "'");
            }

            return res.ToString();
        }

        public override string ToString()
        {
            return @"
import { Component, OnInit, OnDestroy, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';

import { RespuestaAPI } from '../../models/RespuestaAPI';
import { " + tabla.name + @" } from '../../models/" + tabla.name + @"';
import { " + tabla.name + @"Service } from '../../services/" + tabla.name.ToLower() + @".service';

@Component({
  selector: '" + tabla.name.ToLower() + @"-component',
  templateUrl: './" + tabla.name.ToLower() + @".component.html',
  styleUrl: './" + tabla.name.ToLower() + @".component.scss',
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatCardModule, MatButtonModule, MatGridListModule, MatSelectModule, MatInputModule, MatFormFieldModule, MatDividerModule],
})

export class " + name + @"Component implements OnInit, OnDestroy {
  constructor (private " + tabla.name.ToLower() + @"Service: " + tabla.name + @"Service) {
  }

  public _seleccion: " + tabla.name + @" | null = null;
  public dataSource: " + tabla.name + @"[] = [];
  public displayedColumns: string[] = [" + _columnas() + @"];

  ngOnInit() {
    console.log(""ngOnInit " + tabla.name.ToLower() + @""");
    this." + tabla.name.ToLower() + @"Service.get" + tabla.name + @"().subscribe(
      (response) => this.dataSource = ((response as RespuestaAPI<any>).Data) as " + tabla.name + @"[]
      , (error) => console.error('Error:', error)
    );
  }

  ngOnDestroy() {
    console.log(""ngOnDestroy " + tabla.name.ToLower() + @""");
  }

  _agregar(){
    this._seleccion = {} as " + tabla.name + @";
  }

  _seleccionar(elemento: " + tabla.name + @"){
    this._seleccion = elemento;
  }

  _aceptar() {
    this._seleccion = null;
  }

  _cancelar() {
    this._seleccion = null;
  }
}

";
        }
    }
}

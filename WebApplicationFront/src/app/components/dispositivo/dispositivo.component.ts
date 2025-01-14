
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
import { Dispositivo } from '../../models/Dispositivo';
import { DispositivoService } from '../../services/dispositivo.service';

@Component({
  selector: 'dispositivo-component',
  templateUrl: './dispositivo.component.html',
  styleUrl: './dispositivo.component.scss',
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatCardModule, MatButtonModule, MatGridListModule, MatSelectModule, MatInputModule, MatFormFieldModule, MatDividerModule],
})

export class DispositivoComponent implements OnInit, OnDestroy {
  constructor (private dispositivoService: DispositivoService) {
  }

  public _seleccion: Dispositivo | null = null;
  public dataSource: Dispositivo[] = [];
  public displayedColumns: string[] = ['Id', 'CuentaId', 'Eliminado', 'Modificacion', 'Nombre'];

  ngOnInit() {
    console.log("ngOnInit dispositivo");
    this.dispositivoService.getDispositivo().subscribe(
      (response) => this.dataSource = ((response as RespuestaAPI<any>).Data) as Dispositivo[]
      , (error) => console.error('Error:', error)
    );
  }

  ngOnDestroy() {
    console.log("ngOnDestroy dispositivo");
  }

  _agregar(){
    this._seleccion = {} as Dispositivo;
  }

  _seleccionar(elemento: Dispositivo){
    this._seleccion = elemento;
  }

  _aceptar() {
    this._seleccion = null;
  }

  _cancelar() {
    this._seleccion = null;
  }
}


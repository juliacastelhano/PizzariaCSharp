import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import {MatButtonModule} from '@angular/material/button';
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatListModule } from "@angular/material/list";
import { MatCardModule } from "@angular/material/card";
import { FormsModule } from "@angular/forms";
import { MatTableModule } from "@angular/material/table";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";

import { CadastrarAtendenteComponent } from './pages/atendente/cadastrar-atendente/cadastrar-atendente.component';
import { ListarAtendenteComponent } from './pages/atendente/listar-atendente/listar-atendente.component';
import { AlterarAtendenteComponent } from './pages/atendente/alterar-atendente/alterar-atendente.component';
import { ListarCardapioComponent } from './pages/cardapio/listar-cardapio/listar-cardapio.component';
import { CadastrarCardapioComponent } from './pages/cardapio/cadastrar-cardapio/cadastrar-cardapio.component';
import { ListarClienteComponent } from './pages/cliente/listar-cliente/listar-cliente.component';
import { CadastrarClienteComponent } from './pages/cliente/cadastrar-cliente/cadastrar-cliente.component';

@NgModule({
  declarations: [
    AppComponent,
    ListarAtendenteComponent,
    CadastrarAtendenteComponent,
    AlterarAtendenteComponent,
    ListarCardapioComponent,
    CadastrarCardapioComponent,
    ListarClienteComponent,
    CadastrarClienteComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatSnackBarModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    FormsModule,
    MatTableModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

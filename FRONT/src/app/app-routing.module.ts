import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarAtendenteComponent } from './pages/atendente/listar-atendente/listar-atendente.component';
import { CadastrarAtendenteComponent } from './pages/atendente/cadastrar-atendente/cadastrar-atendente.component';
import { AlterarAtendenteComponent } from './pages/atendente/alterar-atendente/alterar-atendente.component';
import { ListarCardapioComponent } from './pages/cardapio/listar-cardapio/listar-cardapio.component';
import { CadastrarCardapioComponent } from './pages/cardapio/cadastrar-cardapio/cadastrar-cardapio.component';
import { ListarClienteComponent } from './pages/cliente/listar-cliente/listar-cliente.component';
import { CadastrarClienteComponent } from './pages/cliente/cadastrar-cliente/cadastrar-cliente.component';


const routes: Routes = [
  {
    path: "pages/atendente/listar-atendente",
    component: ListarAtendenteComponent,
  },
  {
    path: "pages/atendente/cadastrar-atendente",
    component: CadastrarAtendenteComponent,
  },
  {
    path: "pages/atendente/alterar-atendente/:nome",
    component: AlterarAtendenteComponent,
  },
  {
    path: "pages/cardapio/listar-cardapio",
    component: ListarCardapioComponent,
  },
  {
    path: "pages/cardapio/cadastrar-cardapio",
    component: CadastrarCardapioComponent,
  },
  {
    path: "pages/cliente/listar-cliente",
    component: ListarClienteComponent,
  },
  {
    path: "pages/cliente/cadastrar-cliente",
    component: CadastrarClienteComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

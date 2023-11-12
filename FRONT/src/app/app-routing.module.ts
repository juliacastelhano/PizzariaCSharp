import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarAtendenteComponent } from './pages/atendente/listar-atendente/listar-atendente.component';
import { CadastrarAtendenteComponent } from './pages/atendente/cadastrar-atendente/cadastrar-atendente.component';
import { AlterarAtendenteComponent } from './pages/atendente/alterar-atendente/alterar-atendente.component';
import { ListarCardapioComponent } from './pages/cardapio/listar-cardapio/listar-cardapio.component';


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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

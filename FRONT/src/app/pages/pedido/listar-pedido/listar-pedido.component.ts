import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Pedido } from 'src/app/models/pedido.models';

@Component({
  selector: 'app-listar-pedido',
  templateUrl: "./listar-pedido.component.html",
  styleUrls: ["./listar-pedido.component.css"],
})
export class ListarPedidoComponent {
  colunasTabela: string[] = [
	"id",
	"atendente",
	"cliente",
	"cardapio",
	// "carrinho",
	"totalPedido",
	"criadoEm",
	"deletar",
	// "alterar",
  ];

  pedidos: Pedido[] = [];

  constructor(private client: HttpClient, private snackBar: MatSnackBar) {}

  //Método que é executado ao abrir um componente
  ngOnInit(): void {
	this.client
	  .get<Pedido[]>("https://localhost:7288/api/pedido/listar")
	  .subscribe({

		next: (pedidos) => {
		  this.pedidos = pedidos;
		},

		error: (erro) => {
		  console.log(erro);
		},
	  });
  }

  deletar(pedidoId: number) {
	this.client
	  .delete<Pedido[]>(`https://localhost:7288/api/pedido/deletar/${pedidoId}`)
	  .subscribe({
		next: (pedidos) => {
		  this.pedidos = pedidos;
		  this.snackBar.open("Pedido deletado com sucesso!!", "OK", {
			duration: 2000,
			horizontalPosition: "left",
			verticalPosition: "top",
		  });
		  this.ngOnInit();
		},

		error: (erro) => {
		  console.log(erro);
		},
   });
 }
}
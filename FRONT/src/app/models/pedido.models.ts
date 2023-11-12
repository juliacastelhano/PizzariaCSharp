import { Atendente } from "./atendente.models";
import { Cardapio } from "./cardapio.models";
import { Carrinho } from "./carrinho.models";
import { Cliente } from "./cliente.models";

export interface Pedido {
    pedidoId? : number;
    atendente? : Atendente;
    atendenteId : number;
    cliente? : Cliente;
    clienteId : number;
    cardapio? : Cardapio;
    cardapioId : number;
    carrinho? : Carrinho;
    carrinhoId : number;
    totalPedido : number;
    criadoEm? : string;
}
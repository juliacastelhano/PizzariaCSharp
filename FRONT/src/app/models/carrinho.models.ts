import { Cardapio } from "./cardapio.models";
import { Cliente } from "./cliente.models";

export interface Carrinho {
    carrinhoId? : number;
    cliente? : Cliente;
    clienteId : number;
    cardapio? : Cardapio;
    cardapioId : number;
    quantidade : number;
    totalPedido? : number;
    criadoEm? : string;
}
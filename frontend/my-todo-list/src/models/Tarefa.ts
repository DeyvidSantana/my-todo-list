export default class Tarefa {
    constructor (descricao: string, finalizada: boolean) {
        this.Descricao = descricao;
        this.Finalizada = finalizada;
    }

    Id?: number;
    Descricao?: string;
    Finalizada?: boolean;
}
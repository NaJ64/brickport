import { ICommand } from "../command";
import { ICommandHandler } from "../command-handler";

export interface ICreateGameCommand extends ICommand { 
    dateUtc: string;
    players: IPlayerAndColor[];
}

export interface IPlayerAndColor {
    playerName: string | null;
    color: string | null;
}

export class CreateGameCommand implements ICreateGameCommand {
    dateUtc!: string;
    players!: IPlayerAndColor[];
    constructor(state?: ICreateGameCommand) {
        if (state) {
            Object.assign(this, state);
        }
        this.dateUtc = this.dateUtc || new Date().toISOString();
        this.players = this.players || [];
    }
}

export interface ICreateGameHandler extends ICommandHandler<CreateGameCommand, string> { }
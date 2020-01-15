export interface IPlayer {
    playerId: string | null;
    playerName: string | null;
}

export class Player implements IPlayer {
    playerId!: string | null;
    playerName!: string | null;
    constructor(state?: IPlayer) {
        if (state) {
            Object.assign(this, state);
        }
        this.playerId = this.playerId || null;
        this.playerName = this.playerName || null;
    }
}

export interface IPlayerQueries {
    getAsync(): Promise<IPlayer[]>;
    getAsync(id: string): Promise<IPlayer>;
}
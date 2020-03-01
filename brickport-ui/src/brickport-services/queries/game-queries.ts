export interface IGameSummary {
    id: string | null;
    dateUtc: string;
    winner: string | null;
    inProgress: boolean;
    playerScores: IPlayerScoreSummary[];
}

export interface IPlayerScoreSummary {
    playerId: string | null;
    playerName: string | null;
    color: string | null;
    victoryPoints: number;
}

export class GameSummary implements IGameSummary {
    id!: string | null;
    dateUtc!: string;
    winner!: string | null;
    inProgress!: boolean;
    playerScores!: IPlayerScoreSummary[];
    constructor(state?: IGameSummary) {
        if (state) {
            Object.assign(this, state);
        }
        this.id = this.id || null;
        this.dateUtc = this.dateUtc || new Date().toISOString();
        this.winner = this.winner || null;
        this.inProgress = this.inProgress || false;
        this.playerScores = this.playerScores || [];
    }
}

export interface IGameQueries {
    summaryAsync(): Promise<IGameSummary[]>;
    summaryAsync(id: string): Promise<IGameSummary>;
    summaryByDateAsync(startUtc: string | null, endUtc: string | null): Promise<IGameSummary[]>;
}
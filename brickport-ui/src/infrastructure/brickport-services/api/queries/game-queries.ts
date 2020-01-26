import { IGameQueries, IGameSummary } from "../../../../brickport-services/queries/game-queries"
import { BrickportApiClient } from "../api-client";

export class ApiGameQueries implements IGameQueries {
    
    private readonly _apiClient: BrickportApiClient;
    
    constructor(endpoint: string) {
        this._apiClient = new BrickportApiClient(endpoint, true);
    }

    summaryAsync(): Promise<IGameSummary[]>;    
    summaryAsync(id: string): Promise<IGameSummary>;
    summaryAsync(id?: string): Promise<IGameSummary | IGameSummary[]> {
        if (id == null) {
            return this._apiClient.get<IGameSummary[]>();
        }
        return this._apiClient.get<IGameSummary>(`/${id}`);
    }

    summaryByDateAsync(startUtc: string | null, endUtc: string | null): Promise<IGameSummary[]> {
        throw new Error("Method not implemented.");
    }


}
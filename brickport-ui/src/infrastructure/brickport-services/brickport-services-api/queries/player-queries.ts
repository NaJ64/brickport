import { IPlayerQueries, IPlayer } from "../../../../brickport-services/queries/player-queries";
import { BrickportApiClient } from "../api-client";

export class ApiPlayerQueries implements IPlayerQueries {
    
    private readonly _apiClient: BrickportApiClient;
    
    constructor(endpoint: string) {
        this._apiClient = new BrickportApiClient(endpoint, true);
    }

    getAsync(): Promise<IPlayer[]>;
    getAsync(id: string): Promise<IPlayer>;
    getAsync(id?: any): Promise<IPlayer | IPlayer[]> {
        if (id == null) {
            return this._apiClient.get(`/${id}`);
        }
        return this._apiClient.get();
    }
    
}
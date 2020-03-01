import { IRunTestHandler, IRunTestCommand } from "src/brickport-services";
import { BrickportApiClient } from "../api-client";

export class ApiRunTestHandler implements IRunTestHandler {
    
    private readonly _apiClient: BrickportApiClient;
    
    constructor(endpoint: string) {
        this._apiClient = new BrickportApiClient(endpoint, true);
    }

    async handleAsync(command: IRunTestCommand): Promise<string> {
        return await this._apiClient.post<string>(command);
    }

}
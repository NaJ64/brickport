import { Action, Module, Mutation, VuexModule } from 'vuex-module-decorators';
import { IGameQueries, IGameSummary } from "../../brickport-services/queries/game-queries";
import { IPlayer, IPlayerQueries } from "../../brickport-services/queries/player-queries";
import { ApiGameQueries } from "../../infrastructure/brickport-services/api/queries/game-queries";
import { ApiPlayerQueries } from "../../infrastructure/brickport-services/api/queries/player-queries";
import Store from '../index';

export const SET_GAME_SUMMARIES = 'SET_GAME_SUMMARIES';
export const SET_PLAYERS = 'SET_PLAYERS';

@Module({
  dynamic: true,
  name: 'brickport-services',
  namespaced: true,
  store: Store
})
export default class BrickportServicesStoreModule extends VuexModule {

  private readonly _endpoint: string = `${window.location.origin}/api`;
  private readonly _gameQueries: IGameQueries = new ApiGameQueries(`${this._endpoint}/games`);
  private readonly _playerQueries: IPlayerQueries = new ApiPlayerQueries(`${this._endpoint}/players`)

  public players: IPlayer[] = [];
  public gameSummaries: IGameSummary[] = [];

  @Mutation
  public [SET_GAME_SUMMARIES](gameSummaries: IGameSummary[]) {
    this.gameSummaries.length = 0;
    gameSummaries.forEach(gameSummary => this.gameSummaries.push(gameSummary));
  }

  @Action({ commit: SET_GAME_SUMMARIES })
  public async fetchGamesAsync() {
    return await this._gameQueries.summaryAsync();
  }

  @Mutation
  public [SET_PLAYERS](players: IPlayer[]) {
    this.players.length = 0;
    players.forEach(player => this.players.push(player));
  }

  @Action({ commit: SET_PLAYERS })
  public async fetchPlayersAsync() {
    return await this._playerQueries.getAsync();
  }

}
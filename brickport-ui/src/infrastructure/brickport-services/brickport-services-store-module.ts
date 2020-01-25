import { Action, Module, Mutation, VuexModule } from 'vuex-module-decorators';
import { IGameSummary, IGameQueries } from "../../brickport-services/queries/game-queries";
import { IPlayer, IPlayerQueries } from "../../brickport-services/queries/player-queries";
import Store from '../../store/index';
import { ApiGameQueries } from "./brickport-services-api/queries/game-queries";
import { ApiPlayerQueries } from "./brickport-services-api/queries/player-queries";

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
  public SET_GAME_SUMMARIES(gameSummaries: IGameSummary[]) {
    this.gameSummaries.length = 0;
    gameSummaries.forEach(gameSummary => this.gameSummaries.push(gameSummary));
  }

  @Mutation
  public SET_PLAYERS(players: IPlayer[]) {
    this.players.length = 0;
    players.forEach(player => this.players.push(player));
  }

  @Action({ commit: 'SET_GAME_SUMMARIES' })
  public async fetchGamesAsync() {
    return await this._gameQueries.summaryAsync();
  }

  @Action({ commit: 'SET_PLAYERS' })
  public async fetchPlayersAsync() {
    return await this._playerQueries.getAsync();
  }

}
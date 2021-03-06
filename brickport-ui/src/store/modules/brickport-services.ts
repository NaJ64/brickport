import { ICreateGameCommand, IRunTestCommand, IRunTestHandler, RunTestCommand } from "src/brickport-services";
import { ApiRunTestHandler } from "src/infrastructure/brickport-services/api/commands/run-test-handler";
import { Module as IModule } from "vuex";
import { Action, Module, Mutation, VuexModule } from 'vuex-module-decorators';
import { IGameQueries, IGameSummary } from "../../brickport-services/queries/game-queries";
import { IPlayer, IPlayerQueries } from "../../brickport-services/queries/player-queries";
import { ApiGameQueries } from "../../infrastructure/brickport-services/api/queries/game-queries";
import { ApiPlayerQueries } from "../../infrastructure/brickport-services/api/queries/player-queries";
import Store from '../index';

export const SET_RECENT_GAMES = 'SET_RECENT_GAMES';
export const SET_SEARCHED_GAMES = 'SET_SEARCHED_GAMES';
export const SET_SELECTED_GAME = 'SET_SELECTED_GAME';
export const SET_PLAYERS = 'SET_PLAYERS';
export const SET_NEW_GAME = 'SET_NEW_GAME';
export const SET_RESULT = 'SET_RESULT';

@Module({
  dynamic: true,
  name: 'brickport-services',
  namespaced: true,
  store: Store
})
export default class BrickportServicesStoreModule extends VuexModule {

  private readonly _endpoint: string = `${window.location.origin}/api`;
  private readonly _gameQueries: IGameQueries;
  private readonly _playerQueries: IPlayerQueries;
  private readonly _runTestHandler: IRunTestHandler;

  public recentGames: IGameSummary[] = [];
  public searchedGames: IGameSummary[] = [];
  public selectedGame: IGameSummary | null = null;
  public newGame: ICreateGameCommand | null = null;

  public players: IPlayer[] = [];

  public testResult: any | null = null;

  public constructor(module: IModule<ThisType<any>,any>) {
    super(module);
    this._endpoint = `https://localhost:5001/api`;
    this._gameQueries = new ApiGameQueries(`${this._endpoint}/games`);
    this._playerQueries = new ApiPlayerQueries(`${this._endpoint}/players`)
    this._runTestHandler = new ApiRunTestHandler(`${this._endpoint}/test`);
  }

  @Action({ commit: SET_RESULT })
  public async runTestAsync() {
    const runTest: IRunTestCommand = new RunTestCommand({
      dateUtc: new Date().toISOString()
    });
    return await this._runTestHandler.handleAsync(runTest);
  }

  @Action({ commit: SET_RECENT_GAMES })
  public async fetchRecentGamesAsync() {
    const games = await this._gameQueries.summaryAsync();
    return games.slice(0, games.length < 6 ? games.length : 5);
  }

  @Action({ commit: SET_SEARCHED_GAMES })
  public async fetchGamesAsync(search?: { start?: string, end?: string }) {
    if (search) {
      return await this._gameQueries.summaryByDateAsync(search.start || null, search.end || null);
    } else {
      return await this._gameQueries.summaryAsync();
    }
  }

  @Action({ commit: SET_SELECTED_GAME })
  public async fetchSelectedGameAsync(id: string) {
    return await this._gameQueries.summaryAsync(id);
  }

  @Action
  public startNewGame() {
    this.context.commit(SET_SELECTED_GAME);
    const newGame: ICreateGameCommand = {
      dateUtc: new Date().toISOString(),
      players: []
    }
    this.context.commit(SET_NEW_GAME, newGame);
  }

  @Action ({ commit: SET_SELECTED_GAME })
  public clearSelectedGame() {
    return null;
  }

  @Action({ commit: SET_PLAYERS })
  public async fetchPlayersAsync() {
    return await this._playerQueries.getAsync();
  }

  @Mutation
  public [SET_RECENT_GAMES](games: IGameSummary[]) {
    this.recentGames.length = 0;
    games.forEach(game => this.recentGames.push(game));
  }
  
  @Mutation
  public [SET_SEARCHED_GAMES](games: IGameSummary[]) {
    this.searchedGames.length = 0;
    games.forEach(game => this.searchedGames.push(game));
  }

  @Mutation
  public [SET_SELECTED_GAME](game: IGameSummary | null) {
    this.selectedGame = game;
  }

  @Mutation
  public [SET_NEW_GAME](newGame: ICreateGameCommand | null) {
    this.newGame = newGame || null
  }

  @Mutation
  public [SET_PLAYERS](players: IPlayer[]) {
    this.players.length = 0;
    players.forEach(player => this.players.push(player));
  }

  @Mutation
  public [SET_RESULT](result: any | null) {
    this.testResult = result;
  }

}
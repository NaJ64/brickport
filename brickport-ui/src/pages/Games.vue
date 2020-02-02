<template>
  <q-page class="bg-primary flex flex-center">
    <div v-if="!!selectedGame">
      <ul>
        <li>
          {{ JSON.stringify(selectedGame) }}
        </li>
      </ul>
    </div>
    <div v-if="!selectedGame && newGame">
      <span>Create New Game</span>
    </div>
  </q-page>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import { Watch } from 'vue-property-decorator';
import { ICreateGameCommand } from '../brickport-services/commands/create-game';
import { IGameSummary } from '../brickport-services/queries/game-queries';
import BrickportServicesStoreModule from '../store/modules/brickport-services';

@Component
export default class PageGames extends Vue {

  private static readonly isNew = (id: string) => id && !!~["new", "add", "create"].indexOf(id);
  private readonly store = getModule(BrickportServicesStoreModule);

  get selectedGame(): IGameSummary | null {
    return this.store.selectedGame;
  }

  get newGame(): ICreateGameCommand | null {
    return this.store.newGame;
  }

  created() {
    this.onRouteChanged();
  }

  @Watch("$route")
  private onRouteChanged() {
    const id = this.$route.params['id'] || null;
    if (!id) {
      this.store.clearSelectedGame();
      return;
    }
    if (PageGames.isNew(id)) {
      this.store.startNewGame();
    } else {
      this.store.fetchSelectedGameAsync(id);
    }
  }
  

}
</script>
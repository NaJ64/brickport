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
      <create-new-game></create-new-game>
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
import CreateNewGame from '../components/create-new-game.vue';

@Component({
  components: {
    'create-new-game': CreateNewGame
  }
})
export default class Games extends Vue {

  private readonly store = getModule(BrickportServicesStoreModule);

  private static isNew(id: string) {
    return id && !!~['new', 'add', 'create'].indexOf(id);
  }

  get selectedGame(): IGameSummary | null {
    return this.store.selectedGame;
  }

  get newGame(): ICreateGameCommand | null {
    return this.store.newGame;
  }

  created() {
    this.onRouteChanged();
  }

  @Watch('$route')
  private onRouteChanged() {
    const id = this.$route.params['id'] || null;
    if (!id) {
      this.store.clearSelectedGame();
      return;
    }
    if (Games.isNew(id)) {
      this.store.startNewGame();
    } else {
      this.store.fetchSelectedGameAsync(id);
    }
  }
  
}
</script>
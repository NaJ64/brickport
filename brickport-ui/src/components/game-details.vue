<template>
<div>
  {{ JSON.stringify(selectedGame) }}
</div>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import BrickportServicesStoreModule from '../store/modules/brickport-services';
import { IGameSummary } from '../brickport-services/queries/game-queries';

@Component
export default class GameDetails extends Vue {

  private readonly store: BrickportServicesStoreModule = getModule(BrickportServicesStoreModule);
    
  get selectedGame (): IGameSummary | null {
    return this.store.selectedGame;
  }

}
</script>
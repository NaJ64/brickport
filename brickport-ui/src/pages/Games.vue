<template>
  <q-page class="bg-primary flex flex-center">
    <ul id="example-1">
      <li v-for="game in games" v-bind:key="game.id">
        {{ JSON.stringify(game) }}
      </li>
    </ul>
  </q-page>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import { IGameSummary } from '../brickport-services/queries/game-queries';
import BrickportServicesStoreModule from '../store/modules/brickport-services';

@Component
export default class PageGames extends Vue {
  
  private readonly store = getModule(BrickportServicesStoreModule);

  constructor() {
    super();
    this.store.fetchGamesAsync();
  }

  get games(): IGameSummary[] {
    return this.store.gameSummaries;
  }

}
</script>
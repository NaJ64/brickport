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
import { BrickportApiClient } from '../infrastructure/brickport-services/brickport-services-api/api-client';
import { IGameSummary } from '../brickport-services/queries/game-queries';

@Component
export default class PageGames extends Vue {

  private games: IGameSummary[] = [];

  constructor() {
    super();
    const apiClient = new BrickportApiClient(window.location.origin + '/api/games', true);
    apiClient.get<IGameSummary[]>()
      .then(games => games.forEach(game => this.games.push(game)))
  }

}
</script>
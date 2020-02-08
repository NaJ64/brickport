<template>
  <q-page class="bg-primary flex flex-center">
    <ul id="example-1">
      <li v-for="player in players" v-bind:key="player.id">
        {{ JSON.stringify(player) }}
      </li>
    </ul>
  </q-page>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import { IPlayer } from '../brickport-services/queries/player-queries';
import BrickportServicesStoreModule from '../store/modules/brickport-services';

@Component
export default class Players extends Vue { 

  private readonly store = getModule(BrickportServicesStoreModule);

  constructor() {
    super();
    this.store.fetchPlayersAsync();
  }

  get players(): IPlayer[] {
    return this.store.players;
  }
  
}
</script>
<template>
  <q-page class="bg-primary flex flex-center">
      Players
  </q-page>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import { IPlayer } from '../brickport-services/queries/player-queries';
import BrickportServicesStoreModule from '../infrastructure/brickport-services/brickport-services-store-module';

@Component
export default class PagePlayers extends Vue { 

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
<template>
  <q-layout view="hHh LpR fFf">

    <q-header reveal elevated class="bg-dark" height-hint="98">
      <q-toolbar class="glossy">
        <q-btn flat to="/">
          <q-avatar>
            <img src="https://cdn.quasar.dev/logo/svg/quasar-logo.svg">
          </q-avatar>
          <q-toolbar-title>BrickPort</q-toolbar-title>
        </q-btn>
        <q-space />
        <q-tabs shrink>
          <q-btn-dropdown auto-close stretch flat label="Games">
            <q-list>
              <q-item clickable tag="a" :to="{ name: 'games', params: { id: 'create' }}">
                <q-item-section avatar>
                  <q-avatar icon="add_box" color="dark" text-color="white" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>Create New Game</q-item-label>
                </q-item-section>
              </q-item>
              <q-separator inset spaced />
              <q-item-label header>Recent</q-item-label>
              <q-item v-for="game in recentGames" v-bind:key="game.id" clickable tag="a" :to="{ name: 'games', params: { id: game.id }}" >
                <q-item-section avatar>
                  <q-avatar icon="casino" color="dark" text-color="white" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>{{ game.winner || '(In-progress)' }}</q-item-label>
                  <q-item-label caption>{{ new Date(game.dateUtc).toLocaleDateString() }}</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
          <q-route-tab to="/players" label="Players" />
        </q-tabs>
      </q-toolbar>

    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component';
import BrickportServicesStoreModule from '../store/modules/brickport-services';
import { getModule } from 'vuex-module-decorators';

@Component
export default class MasterLayout extends Vue {

  private readonly store = getModule(BrickportServicesStoreModule);

  constructor() {
    super();
    this.store.fetchRecentGamesAsync();
  }

  get recentGames() {
    return this.store.recentGames;
  }

}
</script>

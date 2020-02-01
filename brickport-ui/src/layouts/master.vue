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
              <q-item>
                <q-item-section>Game 1</q-item-section>
              </q-item>

              <q-item >
                <q-item-section>Game 2</q-item-section>
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
import LayoutStoreModule from '../store/modules/layout';
import { getModule } from 'vuex-module-decorators';

@Component
export default class MasterLayout extends Vue {

  private readonly store = getModule(LayoutStoreModule);

  get leftDrawerOpen() {
    return this.store.leftDrawerOpen;
  }

  set leftDrawerOpen(value: boolean) {
    this.store.setLeftDrawerOpen(value);
  }

}
</script>

<template>
  <q-form class="q-gutter-md" @submit="onSubmit">

    <div v-for="player of newGame.players" v-bind:key="player.playerName">
      <q-select v-model="player.playerColor" :options="playerColors" label="Player Color" />
    </div>

    <!-- <q-input
      filled
      v-model="name"
      label="Your name *"
      hint="Name and surname"
      lazy-rules
      :rules="[ val => val && val.length > 0 || 'Please type something']"
    />

    <q-input
      filled
      type="number"
      v-model="age"
      label="Your age *"
      lazy-rules
      :rules="[
        val => val !== null && val !== '' || 'Please type your age',
        val => val > 0 && val < 100 || 'Please type a real age'
      ]"
    /> 
    
    <q-toggle v-model="accept" label="I accept the license and terms" /> -->

    <div>
      <q-btn label="Submit" type="submit" color="primary"/>
    </div>
    <div>
      {{ testResult && JSON.stringify(testResult) || '' }}
    </div>
  </q-form>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import BrickportServicesModule from '../store/modules/brickport-services';
import { ICreateGameCommand } from '../brickport-services/commands/create-game';
import { PlayerColor, PLAYER_COLORS } from '../brickport-services/commands/player-colors';

@Component
export default class CreateGame extends Vue {

  private readonly store = getModule(BrickportServicesModule);
  private submitting: boolean = false;

  get playerColors(): PlayerColor[] {
    return PLAYER_COLORS.slice();
  }
  
  get newGame(): ICreateGameCommand | null {
    return this.store.newGame;
  }

  get testResult (): object | null {
    return this.store.testResult;
  }

  onSubmit() {
    if (this.submitting) {
      return;
    }
    this.submitting = true;
    const stopSubmit = () => this.submitting = false;
    this.store.runTestAsync()
      .then(stopSubmit)
      .catch(stopSubmit);
    // if (this.accept !== true) {
    //   this.$q.notify({
    //     color: 'red-5',
    //     textColor: 'white',
    //     icon: 'warning',
    //     message: 'You need to accept the license and terms first'
    //   })
    // } else {
    //   this.$q.notify({
    //     color: 'green-4',
    //     textColor: 'white',
    //     icon: 'cloud_done',
    //     message: 'Submitted'
    //   })
    // }
  }

}
</script>
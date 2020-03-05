<template>
  <q-form class="q-gutter-md" @submit="onSubmit">

    <div class="row">
      <div class="col">
        <q-input 
          v-model="newPlayer.playerName" 
          label="Player name *" 
          lazy-rules
          :rules="[ val => val && val.length > 0 || 'Please enter player name']"
        />
      </div>
      <div class="col">
        <q-select v-model="newPlayer.color" :options="playerColors" label="Player color *" />
      </div>
      <div class="col">
        <q-btn label="Add" v-on:click="onAddNewPlayer" type="button" color="primary"/>
      </div>
    </div>

    <div class="row q-pa-md">
      <div class="col">
        <q-table
          title="Players"
          :data="newGame.players"
          :columns="playerColumns"
          row-key="color"
        />
      </div>
    </div>

    <!-- <div class="row">
      <div class="col">
        <div v-for="player of newGame.players" v-bind:key="player.playerName">
          <q-select v-model="player.playerColor" :options="playerColors" label="Player Color" />
        </div>
      </div>
      <div class="col"></div>
    </div> -->


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
import { ICreateGameCommand, IPlayerAndColor } from '../brickport-services/commands/create-game';
import { PlayerColor, PLAYER_COLORS } from '../brickport-services/commands/player-colors';

@Component
export default class CreateGame extends Vue {

  private readonly store = getModule(BrickportServicesModule);
  private submitting: boolean = false;
  private newPlayer: IPlayerAndColor = { playerName: null, color: 'Red' };
  private playerColumns = [        
    { name: 'color', label: 'Color', field: 'color', sortable: true },
    { name: 'playerName', label: 'Name', field: 'playerName', sortable: true }
  ];

  get playerColors(): PlayerColor[] {
    return PLAYER_COLORS.slice();
  }
  
  get newGame(): ICreateGameCommand | null {
    return this.store.newGame;
  }

  get testResult (): object | null {
    return this.store.testResult;
  }

  onAddNewPlayer() {
    debugger;
    const newPlayer = Object.assign({}, this.newPlayer);
    this.newGame && this.newGame.players.push(newPlayer);
    this.newPlayer.playerName = null;
    this.newPlayer.color = 'Red';
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
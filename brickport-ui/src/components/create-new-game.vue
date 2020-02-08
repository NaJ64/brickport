<template>
  <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
    <q-input
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

    <q-toggle v-model="accept" label="I accept the license and terms" />

    <div>
      <q-btn label="Submit" type="submit" color="primary"/>
      <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
    </div>
  </q-form>
</template>

<script lang="ts">
import Vue from 'vue';
import Component from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import BrickportServicesModule from '../store/modules/brickport-services';
import { ICreateGameCommand } from '../brickport-services/commands/create-game';

@Component
export default class CreateNewGame extends Vue {

  private readonly store = getModule(BrickportServicesModule);

  private name: string | null = null;
  private age: number | null = null;
  private accept: boolean = false;
  
  get newGame(): ICreateGameCommand | null {
    return this.store.newGame;
  }

  onSubmit() {
    if (this.accept !== true) {
      this.$q.notify({
        color: 'red-5',
        textColor: 'white',
        icon: 'warning',
        message: 'You need to accept the license and terms first'
      })
    } else {
      this.$q.notify({
        color: 'green-4',
        textColor: 'white',
        icon: 'cloud_done',
        message: 'Submitted'
      })
    }
  }

  onReset() {
    this.name = null
    this.age = null
    this.accept = false
  }

}
</script>
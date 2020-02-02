import { Module, VuexModule } from 'vuex-module-decorators';
import Store from '../index';

@Module({
  dynamic: true,
  name: 'layout',
  namespaced: true,
  store: Store
})
export default class LayoutStoreModule extends VuexModule { }
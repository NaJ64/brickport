import { RouteConfig } from 'vue-router'

const routes: RouteConfig[] = [
  {
    path: '/',
    component: () => import('layouts/master.vue'),
    children: [
      { path: '', name: 'home', component: () => import('pages/index.vue') },
      { path: 'games/:id', name: 'games', component: () => import('pages/games.vue') },
      { path: 'players', name: 'players', component: () => import('pages/players.vue') },
    ],
  }
];

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/error-404.vue'),
  });
}

export default routes;

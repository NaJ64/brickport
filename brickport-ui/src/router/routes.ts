import { RouteConfig } from 'vue-router'

const routes: RouteConfig[] = [
  {
    path: '/',
    component: () => import('layouts/my-layout.vue'),
    children: [
      { path: '', component: () => import('pages/index.vue') },
      { path: 'games', component: () => import('pages/games.vue') },
      { path: 'players', component: () => import('pages/players.vue') },
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

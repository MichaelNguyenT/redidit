<template>
  <v-app id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link  v-bind:to="{name: 'post-details' }" >Hullo from Post Details!</router-link >
    </div>
    <router-view />
  </v-app>
</template>

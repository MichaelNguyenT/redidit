<template>
  <v-app id="app">
    <div id="nav">
      <v-card elevation="3">
      <v-app-bar flat color="rgba(0, 0, 0, 0)">
      <v-toolbar-title>Menu</v-toolbar-title><v-spacer></v-spacer>
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link  v-bind:to="{name: 'post-details' }" >Hullo from Post Details!</router-link>
      </v-app-bar>
      </v-card>
    </div>
    <router-view />
  </v-app>
</template>

<template>
  <v-app id="app">
    <div id="nav">
      <v-switch
          v-model="$vuetify.theme.dark"
          label="Light/Dark Mode"
        ></v-switch>
      <v-card elevation="3">
      <v-app-bar flat>
      <v-toolbar-title><v-img
  lazy-src="https://picsum.photos/id/11/10/6"
  max-height="54"
  max-width="250"
  src="https://i.ibb.co/MnvSs9j/Redidit-mockup-logo-6-removebg-preview.png" alt="Redidit-mockup-logo-6-removebg-preview" border="0"
></v-img>
      </v-toolbar-title><v-spacer></v-spacer>
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp; | &nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      </v-app-bar>
      </v-card>
    </div>
    <router-view />
    
  </v-app>
</template>

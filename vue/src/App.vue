<template>
  <v-app id="app">
    <div id="nav">
      <v-switch
        v-model="$vuetify.theme.dark"
        label="Light/Dark Mode"
      ></v-switch>
      <v-card elevation="3">
        <v-app-bar flat>
          <v-toolbar-title
            ><v-img
              lazy-src="https://i.ibb.co/FWJ4rBV/Redidit-final-logo.png"
              max-height="54"
              max-width="300"
              src="https://i.ibb.co/7zVtfG4/Redidit-final-logo.png"
              alt="Redidit-final-logo"
              border="0"
              contain
              align-self-strech
            ></v-img> </v-toolbar-title
          ><v-spacer></v-spacer>
          <h2 v-if="$store.state.token != ''">Hello, {{ this.$store.state.user.username }}!</h2>
          <router-link class="pa-2" v-bind:to="{ name: 'home' }"
            >Home</router-link
          >
          <router-link
            class="pa-2"
            v-bind:to="{ name: 'login' }"
            v-if="$store.state.token == ''"
            >Login</router-link
          >
          <router-link
            class="pa-2"
            v-bind:to="{ name: 'myaccount' }"
            v-if="$store.state.token != ''"
            >MyAccount</router-link
          >
          
          <router-link
            class="pa-2"
            v-bind:to="{ name: 'logout' }"
            v-if="$store.state.token != ''"
            >Logout</router-link
          >
        </v-app-bar>
      </v-card>
    </div>
    <router-view />
  </v-app>
</template>

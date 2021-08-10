<template>
  <div id="register" class="text-center">
    <v-form class="form-register" @submit.prevent="register">
      <v-container>
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <v-row>
      <v-col cols="12" sm="6" md="3">
      <v-text-field label="Username" outlined class="sr-only form-control" 
      v-model="user.username" 
      type="text" 
      id="username"  
      placeholder="Username" 
      required 
      autofocus
      >
      
      </v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="3">
      <v-text-field label="Password" outlined class="sr-only form-control" 
      v-model="user.password" 
      type="password" 
      id="password" 
      placeholder="Password" 
      required
      >
      </v-text-field>
      </v-col>
      <v-col cols="12" sm="6" md="3">
      <v-text-field label="Confirm Password" outlined class="sr-only form-control" 
      v-model="user.confirmPassword"
      type="password" 
      id="confirmPassword" 
      placeholder="Confirm Password" 
      required
      >
      </v-text-field>
      </v-col>
      </v-row>
      <router-link :to="{ name: 'login' }">Have an account? </router-link>
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
      </v-container>
    </v-form>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style></style>

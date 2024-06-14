<script setup lang="ts">
// System Utils
import { ref } from 'vue';

// Get runtime configuration
const config = useRuntimeConfig();

// Get the site's name
const siteName = config.publicsiteName ?? '';
</script>

<template>
  <div class="vc-top-bar">
    <div class="vc-top-bar-container">
      <div class="vc-top-bar-logo">
        <NuxtLink v-bind="{ to: '/', 'aria-label': $t('main_page') }">
          {{ siteName }}
        </NuxtLink>
      </div>
      <div class="vc-top-bar-menu"></div>
      <div class="vc-top-bar-auth">
        <NuxtLink
          to="/auth/signin"
          class="vc-auth-sign-in"
          :aria-label="$t('sign_in')"
        >
          {{ $t('sign_in') }}
        </NuxtLink>
        <NuxtLink
          to="/auth/registration"
          class="vc-auth-sign-up"
          :aria-label="$t('sign_up')"
        >
          {{ $t('sign_up') }}
        </NuxtLink>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// System Utils
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

// Get runtime configuration
const config = useRuntimeConfig();

// Get the site's name
const siteName = config.publicsiteName ?? '';

// Get the site's url
const siteUrl = config.publicsiteUrl ?? '';
</script>

<template>
  <div class="vc-top-bar">
    <div class="vc-top-bar-container">
      <div class="vc-top-bar-logo">
        <NuxtLink
          v-bind="{ to: siteUrl, 'aria-label': $t('main_page') }"
          external
        >
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

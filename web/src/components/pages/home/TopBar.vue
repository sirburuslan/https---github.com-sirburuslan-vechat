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

// Get i18n functions
const { t } = useI18n();

// Create list with menu items
const menuItems = ref([
  { text: t('features'), link: '#features' },
  { text: t('pricing'), link: '#pricing' },
  { text: t('faq'), link: '#faq' },
]);
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
      <div class="vc-top-bar-menu">
        <ul>
          <li v-for="item in menuItems" :key="item.text">
            <NuxtLink
              href="#"
              v-bind="{ 'aria-label': item.text }"
              :data-id="item.link"
              v-scroll-page
              external
            >
              {{ item.text }}
            </NuxtLink>
          </li>
        </ul>
      </div>
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

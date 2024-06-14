<script lang="ts" setup>
// System Utils
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

// Get i18n functions
const { t } = useI18n();

// Create list with stats
const stats = ref([
  { text: t('connected_websites'), value: '5,000,000 +' },
  { text: t('open_threads'), value: '1,000,000 +' },
  { text: t('happy_users'), value: '5,000 +' },
]);
</script>

<template>
  <div class="vc-stats">
    <div class="vc-stats-container">
      <div class="vc-stat">
        <NuxtLink to="https://example.com" target="_blank" external>
          <Icon name="play_circle" />
          {{ $t('watch_demo') }}
        </NuxtLink>
      </div>
      <div v-for="stat in stats" class="vc-stat" :key="stat.text">
        <h3>{{ stat.value }}</h3>
        <h6>{{ stat.text }}</h6>
      </div>
    </div>
  </div>
</template>

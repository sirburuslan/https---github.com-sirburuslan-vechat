<script lang="ts" setup>
// System Utils
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

// Get runtime configuration
const config = useRuntimeConfig();

// Get the site's name
const siteName = config.publicsiteName ?? '';

// Get i18n functions
const { t } = useI18n();

// Create the features list
const features = ref([
  {
    icon: 'support_agent',
    feature: t('24_7_support'),
    description: t('24_7_support_description'),
  },
  {
    icon: 'payments',
    feature: t('scaled_price'),
    description: t('scaled_price_description'),
  },
  {
    icon: 'translate',
    feature: t('multi_language_support'),
    description: t('multi_language_support_description'),
  },
  {
    icon: 'schedule_send',
    feature: t('notifications'),
    description: t('notifications_description'),
  },
  {
    icon: 'history',
    feature: t('chat_history'),
    description: t('chat_history_description'),
  },
  {
    icon: 'location_on',
    feature: t('clients_location'),
    description: t('clients_location_description'),
  },
  {
    icon: 'crop_original',
    feature: t('attachments'),
    description: t('attachments_description'),
  },
  {
    icon: 'touch_app',
    feature: t('easy_to_use'),
    description: t('easy_to_use_description'),
  },
]);
</script>

<template>
  <div class="vc-features">
    <div class="vc-features-container">
      <div class="w-full">
        <h2>
          {{
            $t('what_makes_site_so_different').replace('[site_name]', siteName)
          }}
        </h2>
      </div>
      <div class="w-full">
        <ul class="vc-features-list">
          <li v-for="feature in features" :key="feature.feature">
            <Icon :name="feature.icon" />
            <h4>{{ feature.feature }}</h4>
            <p>{{ feature.description }}</p>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

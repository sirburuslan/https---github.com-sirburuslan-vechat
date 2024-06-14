<script setup lang="ts">
// System Utils
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

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
  <div class="vc-presentation">
    <div class="vc-presentation-container">
      <div class="vc-presentation-text">
        <h1>
          {{ $t('start_now_to_offer') }}
          <span>{{ $t('real_time') }}</span
          ><br />
          {{ $t('support_to_your_clients') }}
        </h1>
        <h4>
          {{ $t('add_live_chat_to_your_website') }}<br />
          {{ $t('and_use_with_no_restrictions') }}
        </h4>
        <NuxtLink
          to="/auth/registration"
          class="vc-get-started"
          :aria-label="$t('book_a_call_now')"
          external
        >
          {{ $t('get_started') }}
        </NuxtLink>
        <NuxtLink
          href="#"
          class="vc-book-call"
          :aria-label="$t('book_a_call_now')"
          external
        >
          {{ $t('book_a_call_now') }}
        </NuxtLink>
        <h6>
          <span>
            <Icon name="tour" />
            {{ $t('set_up_in_minutes') }}
          </span>
          <span>
            <Icon name="tour" />
            {{ $t('no_credit_card_required') }}
          </span>
        </h6>
      </div>
      <div class="vc-presentation-image">
        <NuxtImg
          preload
          src="https://i.imgur.com/eRvQaq5.png"
          placeholder="placeholder.png"
          alt="Presentation"
          sizes="100vw"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
// System Utils
import { ref } from 'vue';

// App Utils
import { useI18n } from 'vue-i18n';

// Get i18n functions
const { t } = useI18n();

// Create list with questions and answers
const questions = ref([
  {
    question: t('how_to_create_chat'),
    answer: t('how_to_create_chat_answer'),
    isExpanded: false,
  },
  {
    question: t('create_chats'),
    answer: t('create_chats_answer'),
    isExpanded: false,
  },
  {
    question: t('receive_notifications'),
    answer: t('receive_notifications_answer'),
    isExpanded: false,
  },
  {
    question: t('add_my_team'),
    answer: t('add_my_team_answer'),
    isExpanded: false,
  },
  {
    question: t('delete_my_account'),
    answer: t('delete_my_account_answer'),
    isExpanded: false,
  },
  {
    question: t('get_support'),
    answer: t('get_support_answer'),
    isExpanded: false,
  },
  {
    question: t('do_you_offer_refunds'),
    answer: t('do_you_offer_refunds_answer'),
    isExpanded: false,
  },
]);

// Show or hide question answer
const handleClick = (question) => {
  question.isExpanded = !question.isExpanded;
};
</script>

<template>
  <div class="vc-faq">
    <div class="vc-faq-container">
      <div class="w-full">
        <h2>{{ $t('questions_answers') }}</h2>
      </div>
      <div class="w-full">
        <ul>
          <li v-for="question in questions" :key="question.question">
            <NuxtLink
              to="#"
              :aria-expanded="question.isExpanded.toString()"
              :aria-label="question.question"
              @click.prevent="handleClick(question)"
              external
            >
              <span>
                {{ question.question }}
              </span>
              <Icon name="expand_more" />
            </NuxtLink>
            <div class="vc-faq-answer">
              <p>{{ question.answer }}</p>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

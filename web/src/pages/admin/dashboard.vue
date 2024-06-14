<script lang="ts" setup>
// System Utils
import { useHead } from '#imports';

// Installed Utils
import { useI18n } from 'vue-i18n';

// Get i18n functions
const { t } = useI18n();

// Set the page name
useHead({
    title: t('dashboard')
});

</script>

<template>
  <div class="grid md:grid-cols-1 lg:grid-cols-3 gap-4 vc-dashboard-container">
      <div class="vc-events-section">
          <div class="flex mb-3">
              <div class="w-full">
                  <h2 class="vc-page-title">
                      {{ $t('events') }}
                  </h2>
              </div>
          </div>
          <div class="flex">
              <div class="w-full vc-events">
                  <div class="vc-events-header flex">
                      <div class="vc-events-date-navigator vc-transparent-color">
                          <div class="flex">
                              <div class="flex-none">
                                  <button type="button" class="vc-events-date-previous-date">
                                      <Icon
                                        name="chevron_left"
                                      />
                                  </button>
                              </div>
                              <div class="grow h-14 text-center">
                                  <h4>
 
                                  </h4>
                              </div>
                              <div class="flex-none">
                                  <button type="button" class="vc-events-date-next-date">
                                      <Icon
                                        name="chevron_right"
                                      />
                                  </button>
                              </div>
                          </div>
                      </div>
                      <div class="vc-events-calendar flex justify-between vc-transparent-color" data-expand="false">
                          <Icon
                            name="calendar_month"
                          />                          
                          <button type="button" class="vc-events-calendar-open">
                              <Icon
                                name="expand_more"
                                extraClass="vc-dropdown-arrow-down-icon"
                              />
                          </button>
                          <div class="vc-events-calendar-container">
                          </div>
                      </div>                            
                  </div>
                  <div class="vc-events-body flex">
                      <div class="vc-events-list">                          
                          <div class="vc-no-events-found">
                              <p>{{ $t('no_events_were_found') }}</p>
                          </div>                                                          
                      </div>
                  </div>
              </div>
          </div>
      </div>
      <div class="col-span-2">
        <div class="vc-dashboard-widget">
            <div class="vc-dashboard-widget-head flex justify-between">
                <h3>
                    <Icon
                      name="supervisor_account"
                      extraClass="vc-dashboard-widget-icon"
                    />
                    <span class="vc-dashboard-widget-text">
                        {{ $t('users') }}
                    </span>
                </h3>
                <div>
                    <button type="button" class="vc-button">
                        <Icon
                          name="watch_later"
                          extraClass="vc-dashboard-widget-calendar-icon"
                        />
                        <span class="vc-dashboard-widget-dropdown-text">
                            {{ $t('30_days') }}
                        </span>
                        <Icon
                          name="expand_more"
                          extraClass="vc-dashboard-widget-arrow-down-icon"
                        />
                    </button>
                    <Dropdown />                    
                </div>
            </div>
            <div class="vc-dashboard-widget-body">
                <p class="vc-no-results-found">{{ $t('no_users_were_joined') }}</p>
            </div>                    
        </div>
          <div class="vc-dashboard-widget">
              <div class="vc-dashboard-widget-head flex justify-between">
                  <h3>
                      <Icon
                        name="add_shopping_cart"
                        extraClass="vc-dashboard-widget-icon"
                      />
                      <span class="vc-dashboard-widget-text">
                          {{ $t('transactions') }}
                      </span>
                  </h3>
              </div>
              <div class="vc-dashboard-widget-body">
                  <ul class="vc-transactions-list">
                      <li class="vc-no-results-found">{{ $t('no_transactions_were_found') }}</li>                                                          
                  </ul>
              </div>                    
          </div>
      </div>
  </div>
</template>
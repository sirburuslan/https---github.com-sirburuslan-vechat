<script lang="ts" setup>
// System Utils
import { ref, onMounted } from 'vue';

// Installed Utils
import { useI18n } from 'vue-i18n';

// App Utils
import type dropdownItems from '~/interfaces/dropdownItems';

// Create a reference for sidebar
const sidebar = ref(null);

// Get the user store
const userStore = useUserStore();

// Get use user composable
const { successMessage, errorMessage, update } = useUser();

// Handle the click on sidebar button
const handleClick = async () => {
  // Update the store
  userStore.updateSidebarStatus(userStore.sidebarStatus ? 0 : 1);

  // Change the sidebar status
  await update({
    sidebarStatus: userStore.sidebarStatus,
  });

  // Check if success message exists
  if (successMessage.value) {
    if (userStore.sidebarStatus > 0)
      sidebar.value.classList.add('vc-minimized-sidebar');
    else sidebar.value.classList.remove('vc-minimized-sidebar');
  }

  // Check if error message exists
  if (errorMessage.value) {
    // Get the show notification plugin
    const { $showNotification } = useNuxtApp();

    // Show error notification
    $showNotification('error', errorMessage.value);
  }
};

// Run code after component mount
onMounted(() => {
  if (userStore.sidebarStatus > 0)
    sidebar.value.classList.add('vc-minimized-sidebar');
  else sidebar.value.classList.remove('vc-minimized-sidebar');
});

// Get i18n functions
const { t } = useI18n();

// Prepare the Admin Menu
const adminData = (): string => {
  return `<div class="flex vc-user-picture">
    <span class="vc-user-picture-cover">
      <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person-fill" viewBox="0 0 16 16">
        <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6"/>
      </svg>
    </span>
    <div class="ml-3">
      <p class="text-sm">Test User</p>
      <p class="text-sm">administrator</p>
    </div>
  </div>`;
};

// Prepare the Admin Menu Items
const adminMenuItems = (): Array<dropdownItems> => {
  return [
    {
      text: t('settings'),
      url: '/admin/settings',
      externalUrl: true
    },
    {
      text: t('sign_out'),
      url: '/auth/signout',
      externalUrl: true
    }
  ];
};
</script>
<template>
  <PageLoading />
  <main class="vc-main">
    <div class="vc-dashboard-sidebar" ref="sidebar">
      <div class="vc-dashboard-sidebar-header">
        <h4>
          <NuxtLink to="/" :aria-label="$t('register_an_account')" external>
            <Icon name="duo" extraClass="vc-sidebar-logo-icon" />
            <span class="vc-sidebar-logo-text"> VeChat </span>
          </NuxtLink>
        </h4>
      </div>
      <div class="vc-dashboard-sidebar-body">
        <ul>
          <li>
            <NuxtLink to="/admin/dashboard" active-class="vc-sidebar-item-active">
              <Icon name="speed" extraClass="vc-dashboard-sidebar-icon" />
              <span class="vc-dashboard-sidebar-menu-item">
                {{ $t('dashboard') }}
              </span>
            </NuxtLink>
          </li>
          <li>
            <NuxtLink to="/admin/users" active-class="vc-sidebar-item-active">
              <Icon name="diversity_3" extraClass="vc-dashboard-sidebar-icon" />
              <span class="vc-dashboard-sidebar-menu-item">
                {{ $t('users') }}
              </span>
            </NuxtLink>
          </li>
          <li>
            <NuxtLink to="/admin/plans" external>
              <Icon
                name="follow_the_signs"
                extraClass="vc-dashboard-sidebar-icon"
              />
              <span class="vc-dashboard-sidebar-menu-item">
                {{ $t('plans') }}
              </span>
            </NuxtLink>
          </li>
          <li>
            <NuxtLink to="/admin/transactions" external>
              <Icon
                name="account_balance_wallet"
                extraClass="vc-dashboard-sidebar-icon"
              />
              <span class="vc-dashboard-sidebar-menu-item">
                {{ $t('transactions') }}
              </span>
            </NuxtLink>
          </li>
        </ul>
      </div>
      <div class="vc-dashboard-sidebar-bottom">
        <Dropdown :items="adminMenuItems()">
          <div v-html="adminData()" />
        </Dropdown>
        <button
          type="button"
          class="vc-user-menu-maximize-minimize-button"
          @click.prevent="handleClick"
        >
          <Icon
            name="keyboard_arrow_right"
            extraClass="vc-user-menu-maximize-icon"
          />
          <Icon
            name="keyboard_arrow_left"
            extraClass="vc-user-menu-minimize-icon"
          />
        </button>
      </div>
    </div>
    <div class="vc-dashboard-content">
      <slot />
    </div>
  </main>
</template>
<style lang="scss">
// Styles for admin dashboard
@import '@/assets/styles/admin/_main.scss';
</style>

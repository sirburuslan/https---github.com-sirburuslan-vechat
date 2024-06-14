<script lang="ts" setup>

// System Utils
import { ref, watch, onMounted, computed, useSlots } from 'vue';

// App Utils
import type dropdownItems from '~/interfaces/dropdownItems';
import { sanitizeInput } from '~/utils/sanitize';
import { useClickOutside } from '~/composables/useClickOutside';

// Get Slots
const slots = useSlots();

// Dropdown State
const dropdown = ref(null);

// Button State
const button = ref(null);

// Menu State
const menu = ref(null);

// Grouped State
const state = reactive({
  slotContent: '',
  isOpen: false
});

// Get the received data from the parent
defineProps({
  items: Array<dropdownItems>
});

// Toggle the dropdown
const toggleDropdown = () => {
  state.isOpen = !state.isOpen;
};

// Composable to detect click outside dropdown
const { isClickOutside } = useClickOutside(dropdown);

// Monitor the reactive changes
watch(state, (newValue, oldValue) => {

  // Check if the menu should be showed
  if ( newValue.isOpen ) {

    setTimeout(() => {
 
      // Get the height
      const height: number = (button.value.$el as HTMLElement).clientHeight;

      // Calculate the height of the button
      const button_height: number = menu.value.offsetHeight;

      // Set transformation
      menu.value.style.marginTop = `-${button_height + height + 10}px`;

    }, 300);

  }

});

// Monitor click outside the dropdown
watch(isClickOutside, (value) => {
  if (value) {
    state.isOpen = false;
    isClickOutside.value = false;
  }
});

// Hook for mounted state
onMounted(() => {
  if ( typeof slots.default === 'function' ) {
    state.slotContent = (slots.default()[0].props?.innerHTML)?sanitizeInput(slots.default()[0].props.innerHTML):slots.default()[0].children;
  }
});

</script>

<template>
  <div class="vc-dropdown" :data-expand="state.isOpen" ref="dropdown">
      <NuxtLink
        href="#"
        class="vc-button flex justify-between"
        external
        @click.prevent="toggleDropdown()"
        ref="button"
        >
        <div v-html="state.slotContent"></div>
        <Icon
          name="expand_more"
          extraClass="vc-dropdown-arrow-down-icon"
        />        
      </NuxtLink>
      <div class="vc-dropdown-menu" ref="menu">
          <ul v-if="items && items.length > 0">
            <li v-for="(item, index) in items" :key="index">
              <NuxtLink
                v-if="item.externalUrl"
                :to="item.url"
                :aria-label="item.text"
                external
              >
                {{ item.text }}
              </NuxtLink>
              <NuxtLink
                v-else
                :to="item.url"
                :aria-label="item.text"
              >
                {{ item.text }}
              </NuxtLink>              
            </li>
          </ul>
          <ul v-else>
            <li class="vc-no-results-found-message">
              {{ $t('no_items_found') }}
            </li>
          </ul>          
      </div>
  </div>
</template>
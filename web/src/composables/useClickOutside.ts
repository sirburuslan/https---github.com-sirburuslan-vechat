// System Utils
import type { Ref } from 'vue';
import { ref, onMounted, onUnmounted } from 'vue';

/**
 * Detect click outside of an element
 * 
 * @param elementRef reference for element
 * 
 * @returns Ref<boolean>
 */
export function useClickOutside(elementRef: { value: { contains: (arg0: EventTarget | null) => any; }; }): { isClickOutside: Ref<boolean>; } {

  // Default status
  const isClickOutside = ref(false);

  /**
   * Handle all clicks
   * 
   * @param event 
   */
  const handleClick = (event: Event) => {
    if (elementRef.value && !elementRef.value.contains(event.target)) {
      isClickOutside.value = true;
    }
  };

  // Execute code after component mounted
  onMounted(() => {
    document.addEventListener('click', handleClick);
  });

  // Execute code after component unmounted
  onUnmounted(() => {
    document.removeEventListener('click', handleClick);
  });

  return { isClickOutside };
  
}

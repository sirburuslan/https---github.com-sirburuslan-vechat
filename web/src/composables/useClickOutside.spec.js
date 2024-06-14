// System Utils
import { ref } from 'vue';

// Installed Utils
import { mount } from '@vue/test-utils';
import {
    describe,
    expect,
    test,
    beforeEach
} from 'vitest';

// App Utils
import { useClickOutside } from '@/composables/useClickOutside';
import Dropdown from '@/components/ui/Dropdown.vue';

describe('useClickOutside', () => {

  test('should return true when is clicked on the element', async () => {
    const wrapper = mount(Dropdown, {
      props: {
        items: [
          {
            text: 'Item 1',
            url: '/item1',
            externalUrl: false
          },
          {
            text: 'Item 2',
            url: '/item2',
            externalUrl: false
          }  
        ]
      }
    });
    const button = wrapper.find('.vc-button');
    await button.trigger('click');
    expect(wrapper.vm.state.isOpen).toBe(true);
    document.body.click();
    await wrapper.vm.$nextTick();
    expect(wrapper.vm.state.isOpen).toBe(false);
  });

})
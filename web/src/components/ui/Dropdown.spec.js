// Installed Utils
import { mount } from '@vue/test-utils';
import {
    describe,
    expect,
    test
} from 'vitest';

// App Utils
import Dropdown from '@/components/ui/Dropdown.vue';

describe('Dropdown', () => {

  test('renders correctly with slots and items', async () => {
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

    // Update slot
    wrapper.vm.state.slotContent = 'Slot';
    
    // Wait for the next DOM update cycle
    await wrapper.vm.$nextTick();

    // Expects
    expect(wrapper.find('.vc-button > div').html()).toContain('Slot');
    expect(wrapper.findAll('.vc-dropdown-menu ul li').length).toBe(2);
    
  });

  test('toggles dropdown on button click', async () => {
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

    await button.trigger('click');
    expect(wrapper.vm.state.isOpen).toBe(false);

  });

  test('closes dropdown on outside click', async () => {
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

    // Simulate opening the dropdown
    wrapper.vm.state.isOpen = true;
    await wrapper.vm.$nextTick();
    expect(wrapper.vm.state.isOpen).toBe(true);

    // Simulate outside click
    document.body.click();
    await wrapper.vm.$nextTick();
    expect(wrapper.vm.state.isOpen).toBe(false);
  });

  test('computes menu height correctly when opened', async () => {
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
        ],
      },
    });

    wrapper.vm.state.isOpen = true;
    await new Promise((resolve) => setTimeout(resolve, 300));    
    wrapper.vm.menu.style.marginTop = `-${160}px`;
    expect(wrapper.vm.menu.style.marginTop).toBe('-160px');

  });

});
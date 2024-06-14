// Installed Utils
import { mount } from '@vue/test-utils';
import {
  describe,
  test,
  expect
} from 'vitest';

// App Utils
import { pageLoadDirective as pageLoad } from '~/directives/pageLoad.directive';

// Create a component only to test the directive
const pageLoadComponent = defineComponent({
  template: `<div class="vc-page-loading" v-page-load>
    <div class="vc-loading-container">
      <div class="vc-loading-circle">
        <div class="vc-loading-circle-box vc-loading-circle-box-complete">
          <div class="vc-loading-circle-fill"></div>
        </div>
        <div class="vc-loading-circle-box">
          <div class="vc-loading-circle-fill"></div>
        </div>
        <div class="vc-loading-text"></div>
      </div>
    </div>
  </div>`,
  directives: {
    pageLoad,
  },
});

// Test the page load directive
describe('pageLoadDirective', () => {

  test('should apply the directive correctly', async () => {

    const wrapper = mount(pageLoadComponent);

    const loadingText = wrapper.find('.vc-loading-text');

    await new Promise((resolve) => setTimeout(resolve, 1600));

    expect(loadingText.text()).toBe('100%');
  });

});

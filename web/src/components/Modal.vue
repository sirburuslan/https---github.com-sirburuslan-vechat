<template>
  <Teleport to="body">
    <div class="vc-modal vc-modal-show vc-modal-md" aria-labelledby="modal-title" role="dialog" aria-modal="true" v-if="visible">
      <div class="fixed inset-0 bg-black bg-opacity-50 transition-opacity vc-modal-cover" @click="closeModal()" role="button" tabindex="0" aria-label="Close modal"></div>
      <div class="vc-modal-container">
        <div class="vc-modal-header flex justify-between">
          <h3>
            {{ title }}
          </h3>
          <button type="button" class="vc-modal-hide-modal" @click="closeModal()">
            <Icon name="close" />
          </button>
        </div>
        <div class="vc-modal-body">
          <slot />
        </div>
      </div>
    </div>    
  </Teleport>
</template>
<script setup>
const props = defineProps({
  title: {
    type: String,
    required: true
  },
  visible: {
    type: Boolean,
    default: false
  }
});
const emit = defineEmits(['update:visible']);
const closeModal = () => {
  emit('update:visible', false);
};
</script>
<style scoped></style>
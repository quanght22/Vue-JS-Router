<template type="x/template" id="modal-template">
    <div class="modal-mask" v-show="show" transition="modal">
        <div class="modal-container">

            <div class="modal-header">
                <h3>New Post</h3>
            </div>

            <div class="modal-body">
                <label class="form-label">
                    Title
                    <input class="form-control">
                </label>
                <label class="form-label">
                    Body
                    <textarea rows="5" class="form-control"></textarea>
                </label>
            </div>

            <div class="modal-footer text-right">
                <button class="modal-default-button" @click="savePost()">
                    Save
                </button>
                <button class="modal-default-button" @click="close()">
                    Cancel
                </button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  name: 'modal',
  template: '#modal-template',
  props:['show'],
    methods: {
    close: function () {
      this.show = false;
    },
    savePost: function () {
      // Insert AJAX call here...
      this.close();
    }
    
  }
}
    
</script>